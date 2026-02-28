import { type StorageLike, useStorage } from '@vueuse/core'
import { ref, type Ref } from 'vue'

type StorageCacheEntry<T> = {
  value: T | null
  expiresAt: number
}

export function useCache<T>(
  key: string,
  options: {
    ttl: number
    storage?: StorageLike
    initialValue?: T
  },
) {
  const { ttl, storage = sessionStorage, initialValue = null } = options
  const entry = useStorage<StorageCacheEntry<T>>(key, { value: initialValue, expiresAt: Date.now() + ttl }, storage)
  const inFlight: Ref<Promise<T> | null> = ref(null)

  const isFresh = () => entry.value.expiresAt > Date.now()

  const get = async (loader: () => Promise<T>): Promise<T> => {
    if (isFresh() && entry.value.value !== null) {
      return entry.value.value
    }

    if (inFlight.value) {
      return await inFlight.value
    }

    inFlight.value = loader()
      .then((value) => {
        entry.value = { value, expiresAt: Date.now() + ttl }
        return value
      })
      .finally(() => (inFlight.value = null))

    return inFlight.value
  }

  const invalidate = () => {
    entry.value.expiresAt = 0
  }

  const clear = () => {
    entry.value = { value: null, expiresAt: 0 }
  }

  return {
    entry,
    isFresh,
    get,
    invalidate,
    clear,
  }
}

export function useCachePool<T>(options: { ttl: number; storage?: StorageLike; initialValue?: T; keyPrefix?: string }) {
  const { keyPrefix = '' } = options
  const pool = new Map<string, ReturnType<typeof useCache<T>>>()

  const forKey = (key: string) => {
    const fullKey = keyPrefix ? `${keyPrefix}:${key}` : key
    let cache = pool.get(fullKey)
    if (!cache) {
      cache = useCache<T>(fullKey, options)
      pool.set(fullKey, cache)
    }
    return cache
  }

  const get = (key: string, loader: () => Promise<T>) => forKey(key).get(loader)

  const invalidate = (key: string) => forKey(key).invalidate()

  const clear = (key: string) => {
    const fullKey = keyPrefix ? `${keyPrefix}:${key}` : key
    pool.get(fullKey)?.clear()
    pool.delete(fullKey)
  }

  const clearAll = () => {
    pool.forEach((cache) => cache.clear())
    pool.clear()
  }

  return {
    forKey,
    get,
    invalidate,
    clear,
    clearAll,
  }
}
