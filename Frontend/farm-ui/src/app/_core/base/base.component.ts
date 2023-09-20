import { Component } from '@angular/core';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent<T> {

  Entity!: T;
  Entities!: T[];
  URL: string = '';

  constructor(
    public baseService: BaseService<T>
  ) {
  }

  getList() {
    this.baseService.getAll(this.URL).subscribe(
      (res) => {
        this.Entities = res.Data;
      }
    );
  }

  save() {
    this.baseService.save(this.URL).subscribe(
      (res) => {
        this.Entity = res.Data;
      }
    );
  }

  getById() {
    this.baseService.getById(this.URL).subscribe(
      (res) => {
        this.Entity = res.Data;
      }
    );
  }
}
