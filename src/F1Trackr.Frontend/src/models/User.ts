export interface UserId {
  value: string
}

export interface User {
  id: UserId
  name: string
  isAdmin: boolean
}
