import { useToast } from 'primevue'

interface HttpResult<TResult> {
  success: boolean
  value?: TResult | null
  errors: Record<string, string[]>
}

const useHttp = () => {
  const toast = useToast()

  function handleUnexpectedError<TResult>(error: unknown): Promise<HttpResult<TResult>> {
    toast.add({
      severity: 'error',
      summary: 'Failed to fetch data',
      detail: 'Failed to fetch data from the server.',
      life: 3000,
    })

    return Promise.reject(error)
  }

  async function handleResponse<TResult>(response: Response): Promise<HttpResult<TResult>> {
    if (response.ok) {
      if (response.status === 204) {
        return {
          success: true,
          errors: {},
        }
      }

      return {
        success: true,
        value: await response.json(),
        errors: {},
      }
    }

    if (response.status === 400) {
      toast.add({
        severity: 'error',
        summary: 'Validation Error',
        detail: 'The request was invalid.',
        life: 3000,
      })

      return {
        success: false,
        errors: await response.json(),
      }
    }

    if (response.status === 403) {
      toast.add({
        severity: 'error',
        summary: 'Access Denied',
        detail: 'You do not have permission to access this resource.',
        life: 3000,
      })
    }

    if (response.status === 404) {
      toast.add({
        severity: 'error',
        summary: 'Not Found',
        detail: 'The requested resource could not be found.',
        life: 3000,
      })
    }

    toast.add({
      severity: 'error',
      summary: 'Failed to fetch data',
      detail: 'Failed to fetch data from the server.',
      life: 3000,
    })

    return Promise.reject(response)
  }

  async function get<TResult>(uri: string): Promise<HttpResult<TResult>> {
    try {
      const response = await fetch(uri)

      return handleResponse<TResult>(response)
    } catch (error) {
      return handleUnexpectedError(error)
    }
  }

  async function post<TBody, TResult>(uri: string, body?: TBody | null): Promise<HttpResult<TResult>> {
    try {
      const response = await fetch(uri, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: body ? JSON.stringify(body) : undefined,
      })

      return handleResponse<TResult>(response)
    } catch (error) {
      return Promise.reject(error)
    }
  }

  async function put<TBody, TResult>(uri: string, body?: TBody | null): Promise<HttpResult<TResult>> {
    try {
      const response = await fetch(uri, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: body ? JSON.stringify(body) : undefined,
      })

      return handleResponse<TResult>(response)
    } catch (error) {
      return Promise.reject(error)
    }
  }

  async function del<TResult>(uri: string): Promise<HttpResult<TResult>> {
    try {
      const response = await fetch(uri, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
        },
      })

      return handleResponse<TResult>(response)
    } catch (error) {
      return Promise.reject(error)
    }
  }

  return { get, post, put, del }
}

export default useHttp
