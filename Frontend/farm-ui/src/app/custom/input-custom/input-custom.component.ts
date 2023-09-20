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
  @Input() LABEL: any = '';
  @Input() ENTITY: any = null;

  @Output() changeInput = new EventEmitter<any>();

  @Input() DISABLED: boolean = false;
  @Input() REQUIRED: boolean = false;
  @Input() MIN: any = null;
  @Input() MAX: any = null;
  @Input() MAXLENGTH: any = null;

  VALID: boolean = true;

  changeValueInput() {
    if (this.REQUIRED) {
      this.VALID = this.VALUE ? true : false;
    }
    this.ENTITY[this.FIELD] = this.VALUE;
    this.changeInput.emit(this.VALID);
  }
}
