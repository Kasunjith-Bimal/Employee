import { RoleType } from "./RoleType"

export interface Employee {
    id: string
    email: string,
    firstName: string,
    lastName: string,
    fullName: string,
    salary: number,
    joinDate: Date,
    address: string,
    telephone: string,
    roleType: RoleType,
    isActive : boolean
  }