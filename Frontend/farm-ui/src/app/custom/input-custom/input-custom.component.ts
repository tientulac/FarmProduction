import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-input-custom',
  templateUrl: './input-custom.component.html',
  styleUrls: ['./input-custom.component.scss']
})
export class InputCustomComponent {
  @Input() TYPE: string = '';
  @Input() PLACEHOLDER: string = '';
  @Input() VALUE: any = null;
  @Input() FIELD: any = '';
  @Input() ENTITY: any = null;
  @Input() DISABLED: any = null;
  @Input() MIN: any = null;
  @Input() MAX: any = null;
  @Input() MAXLENGTH: any = null;
  @Input() REQUIRED: any = null;
  @Output() inputChange = new EventEmitter<string>();

  VALID: boolean = false;

  changeValueInput() {
    this.ENTITY[this.FIELD] = this.VALUE;
    this.inputChange.emit(this.ENTITY);
  }
}
