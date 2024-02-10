import { Pipe, PipeTransform } from '@angular/core';
import { Employee } from '../models/Employee';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {
  transform(employees: Employee[] | null, searchText: string): Employee[] | null {
    if (!employees || !searchText) {
      return employees;
    }

    searchText = searchText.toLowerCase();

    return employees.filter(employee => {
      // Check and concatenate all fields to include in the search, handling null or undefined
      const fieldsToSearch = [
        employee.id,
        employee.email,
        employee.firstName,
        employee.lastName,
        employee.fullName,
        // Convert numbers and dates to string and handle potential nulls for safe inclusion in search
        employee.salary ? employee.salary.toString() : '',
        employee.joinDate ? this.formatDate(employee.joinDate) : '', // Custom formatDate function used for readability
        employee.address,
        employee.telephone,
        // Assuming RoleType is an enum or similar that can be converted to string
        employee.roleType ? employee.roleType.toString() : ''
      ].map(field => field ? field.toLowerCase() : ''); // Convert all fields to lowercase, ensuring empty string for nulls

      // Check if any field includes the searchText
      return fieldsToSearch.some(field => field.includes(searchText));
    });
  }

  // Helper function to format Date objects to a string, could be adjusted based on desired format
  private formatDate(date: Date): string {
    // Example format: YYYY-MM-DD
    const d = new Date(date);
    let month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) 
        month = '0' + month;
    if (day.length < 2) 
        day = '0' + day;

    return [year, month, day].join('-');
  }
}
// This code introduces a formatDate helper function that formats Date objects into a consistent string format (YYYY-MM-DD in this case), which is useful for search comparisons. You may adjust this format based on your requirements or localization needs.

// The main transform function prepares a list of employee attributes for the search by con