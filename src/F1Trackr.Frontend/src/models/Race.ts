import type { ConstructorId } from '@/models/Constructor.ts'
import type { DriverId } from '@/models/Driver.ts'

export interface RaceId {
  season: string
  round: number
}

export interface RaceSummary {
  id: RaceId
  name: string
  circuit: string
  location: string
  country: string
  grandPrixTime: Date
  firstPracticeTime: Date | null
}

export interface DriverPosition {
  driverId: DriverId
  constructorId: ConstructorId
  position: number
  points: number
  grid: number
  laps: number
  status: string
  totalTime?: string | null
  fastestLapTime?: string | null
}

export interface QualifyingPosition {
  driverId: DriverId
  constructorId: ConstructorId
  position: number
  q1?: string | null
  q2?: string | null
  q3?: string | null
}

export interface RaceOverview {
  id: RaceId
  name: string
  circuit: string
  location: string
  country: string
  grandPrixTime: Date
  firstPracticeTime?: Date | null
  secondPracticeTime?: Date | null
  thirdPracticeTime?: Date | null
  qualifyingTime?: Date | null
  sprintQualifyingTime?: Date | null
  sprintTime?: Date | null
  sprintDriverResults?: DriverPosition[]
  qualifyingResults?: QualifyingPosition[]
  driverResults?: DriverPosition[]
  constructorStandingsSnapshot?: Array<{
    constructorId: ConstructorId
    position: number
    points: number
    wins: number
  }>
  driverStandingsSnapshot?: Array<{
    driverId: DriverId
    position: number
    points: number
    wins: number
  }>
}
