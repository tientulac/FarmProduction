import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GROUP_BUTTON } from 'src/app/entities/GroupButton.Entity';

@Component({
  selector: 'app-group-button',
  templateUrl: './group-button.component.html',
  styleUrls: ['./group-button.component.scss']
})
export class GroupButtonComponent {

  @Input() GROUP_BUTTON = new GROUP_BUTTON();

  @Output() openModalEvent = new EventEmitter<any>();
  @Output() searchEvent = new EventEmitter<any>();
  @Output() filterEvent = new EventEmitter<any>();
  @Output() exportExcelEvent = new EventEmitter<any>();

  openModal(type: any, data: any) {
    this.openModalEvent.emit(type);
  }
  search() {
    this.searchEvent.emit();
  }
  filter() {
    this.filterEvent.emit();
  }
  export() {
    this.exportExcelEvent.emit();
  }
  reload() {
    window.location.reload();
  }
}
