export interface DriverId {
  value: string
}

export interface DriverSummary {
  id: DriverId
  givenName: string
  familyName: string
  nationality: string | null
  code: string | null
  driverNumber: string | null
  imageAddress: string | null
}
