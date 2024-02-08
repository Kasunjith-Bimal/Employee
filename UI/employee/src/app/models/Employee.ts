import { RoleType } from "./RoleType"

export interface Employee {
    Id: string
    Email: string,
    FirstName: string,
    LastName: string,
    Salary: number,
    JoinDate: Date,
    Address: string,
    Telephone: string,
    RoleType: RoleType
  }