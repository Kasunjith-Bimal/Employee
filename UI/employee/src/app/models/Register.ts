import { RoleType } from "./RoleType";

export interface Register {
    Email: string,
    FullName: string,
    Salary: number,
    JoinDate: Date,
    Address: string,
    Telephone: string,
    RoleType: RoleType
  }