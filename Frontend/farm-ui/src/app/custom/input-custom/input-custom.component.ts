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
  @Input() CLASSNAME: any = '';
  @Input() ENTITY: any = null;

  @Output() changeInput = new EventEmitter<any>();

  @Input() DISABLED: boolean = false;
  @Input() REQUIRED: boolean = false;
  @Input() MIN: any = null;
  @Input() MAX: any = null;
  @Input() MINLENGTH: any = null;
  @Input() MAXLENGTH: any = null;

  VALID: boolean = true;
  VALID_REQUIRED: boolean = true;
  VALID_MIN_LENGTH: boolean = true;
  VALID_MAX_LENGTH: boolean = true;

  ngOnInit() {
    if (this.ENTITY.id) {
      this.changeValueInput();
    }
  }

  changeValueInput() {
    if (this.REQUIRED) {
      this.VALID_REQUIRED = this.VALUE.toString().length > 0 ? true : false;
    }
    if (this.MINLENGTH) {
      this.VALID_MIN_LENGTH = this.VALUE.toString().length <= this.MINLENGTH ? false : true;
    }
    if (this.MAXLENGTH) {
      this.VALID_MAX_LENGTH = this.VALUE.toString().length >= this.MAXLENGTH ? false : true;
    }
    this.ENTITY[this.FIELD] = this.VALUE;
    this.VALID = this.VALID_REQUIRED && this.VALID_MIN_LENGTH && this.VALID_MAX_LENGTH;
    this.changeInput.emit(this.VALID);
  }
}
