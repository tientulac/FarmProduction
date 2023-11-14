import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-select-custom',
  templateUrl: './select-custom.component.html',
  styleUrls: ['./select-custom.component.scss']
})
export class SelectCustomComponent {
  @Input() TYPE: string = '';
  @Input() PLACEHOLDER: string = '';
  @Input() VALUE: any = null;
  @Input() FIELD: any = '';
  @Input() LABEL: any = '';
  @Input() CLASSNAME: any = '';
  @Input() ENTITY: any = null;
  @Input() LABEL_SELECT: any = null;
  @Input() VALUE_SELECT: any = null;
  @Input() LISTOPTION: any = null;

  @Output() changeSelect = new EventEmitter<any>();

  @Input() DISABLED: boolean = false;
  @Input() REQUIRED: boolean = false;
  @Input() MIN: any = null;
  @Input() MAX: any = null;

  VALID: boolean = true;
  VALID_REQUIRED: boolean = true;

  ngOnInit() {
    if (this.ENTITY.id) {
      this.changeValueSelect();
    }
  }

  changeValueSelect() {
    if (this.REQUIRED) {
      if (this.TYPE == 'NORMAL') {
        this.VALID_REQUIRED = this.VALUE ? true : false;
      }
      else {
        this.VALID_REQUIRED = this.VALUE?.length > 0 ? true : false;
      }
    }
    this.ENTITY[this.FIELD] = this.VALUE;
    this.VALID = this.VALID_REQUIRED;
    this.changeSelect.emit(this.VALID);
  }
}
