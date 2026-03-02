function parseLapTime(value?: string | null): number | null {
  if (!value) return null

  const m = /^(\d{2}):(\d{2}):(\d{2})(?:\.(\d{1,7}))?$/.exec(value.trim())
  if (!m) return null

  const hours = Number(m[1])
  const minutes = Number(m[2])
  const seconds = Number(m[3])
  const frac = (m[4] ?? '').padEnd(7, '0')
  const msFromFrac = Math.floor(Number(frac.slice(0, 3)))

  return ((hours * 60 + minutes) * 60 + seconds) * 1000 + msFromFrac
}

function formatLapTime(value?: string | null): string | null {
  if (!value) return null

  const m = /^(\d{2}):(\d{2}):(\d{2})(?:\.(\d{1,7}))?$/.exec(value.trim())
  if (!m) return null

  const hours = Number(m[1])
  const minutes = Number(m[2])
  const seconds = Number(m[3])
  const frac = (m[4] ?? '').padEnd(7, '0')

  if (hours === 0) return `${minutes}:${String(seconds).padStart(2, '0')}.${frac.slice(0, 3)}`

  return `${hours}:${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}.${frac.slice(0, 3)}`
}

export { formatLapTime, parseLapTime }
