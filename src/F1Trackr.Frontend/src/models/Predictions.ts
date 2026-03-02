interface Constructor {
  id: {
    value: string
  }
  name: string
  nationality?: string | null
  logoAddress?: string | null
}

interface Driver {
  id: {
    value: string
  }
  givenName: string
  familyName: string
  nationality?: string | null
  code?: string | null
  driverNumber?: string | null
  imageAddress?: string | null
}

export interface ConstructorPrediction {
  constructor: Constructor
  predictedPosition: number
}

export interface DriverPrediction {
  driver: Driver
  predictedPosition: number
}

export interface DriverRacePrediction {
  raceId: {
    season: string
    round: number
  }
  drivers: DriverPrediction[]
}
