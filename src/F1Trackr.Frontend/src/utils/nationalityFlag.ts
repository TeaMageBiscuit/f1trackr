const nationalityToCountryCode: Record<string, string> = {
  British: 'gb',
  Dutch: 'nl',
  Monegasque: 'mc',
  Spanish: 'es',
  Mexican: 'mx',
  Australian: 'au',
  German: 'de',
  French: 'fr',
  Canadian: 'ca',
  Thai: 'th',
  Japanese: 'jp',
  Danish: 'dk',
  Finnish: 'fi',
  Chinese: 'cn',
  American: 'us',
  Italian: 'it',
  Brazilian: 'br',
  Austrian: 'at',
  Argentine: 'ar',
  'New Zealander': 'nz',
  Swiss: 'ch',
}

const getFlagUrl = (nationality: string | null | undefined) => {
  const code = nationality ? nationalityToCountryCode[nationality] || 'un' : 'un' // 'un' for unknown
  return `https://flagcdn.com/w40/${code.toLowerCase()}.png`
}

export default getFlagUrl
