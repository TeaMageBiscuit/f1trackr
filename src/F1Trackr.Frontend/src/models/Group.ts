import type { ConstructorPrediction, DriverPrediction, DriverRacePrediction, User, UserId } from '@/models'

export interface GroupId {
  value: string
}

export interface GroupSummary {
  id: GroupId
  name: string
  season: string
}

export interface GroupMemberSummary {
  groupId: GroupId
  userId: UserId
  userName: string
  currentScore: number
}

export interface GroupOverview {
  id: GroupId
  name: string
  season: string
  members: GroupMemberSummary[]
}

export interface GroupMember {
  groupId: GroupId
  userId: UserId
  user: User
  currentScore: number
  scoreSnapshots: GroupMemberScoreSnapshot[]
  constructorPredictions: ConstructorPrediction[]
  driverPredictions: DriverPrediction[]
  driverRacePredictions: DriverRacePrediction[]
}

export interface GroupMemberScoreSnapshot {
  raceId: {
    season: string
    round: number
  }
  basePoints: number
  constructorStandingsPenalty: number
  driverStandingsPenalty: number
  raceBonusThisRound: number
  raceBonusTotal: number
  totalPoints: number
  computedAt: Date
}
