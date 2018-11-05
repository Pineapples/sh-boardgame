import { Pipe, PipeTransform } from '@angular/core';
import {RoleType} from './models/roleType';

@Pipe({
  name: 'roleType'
})
export class RoleTypePipe implements PipeTransform {

  transform(value: any): any {
    return RoleType[value];
  }

}
