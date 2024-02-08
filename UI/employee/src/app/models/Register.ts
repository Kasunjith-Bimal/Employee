import { RoleType } from "./RoleType";

export interface Register {
    Email: string,
    FirstName: string,
    LastName: string,
    Salary: number,
    JoinDate: Date,
    Address: string,
    Telephone: string,
    RoleType: RoleType
  }