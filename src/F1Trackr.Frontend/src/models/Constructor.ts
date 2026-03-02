export interface ConstructorId {
  value: string
}

export interface ConstructorSummary {
  id: ConstructorId
  name: string
  nationality: string | null
  logoAddress: string | null
}
