import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrls: ['./delete-modal.component.scss']
})
export class DeleteModalComponent {
  @Input() SHOWDELETE: any = false;
  @Input() ID: any;
  @Input() TITLE: any;
  @Input() ENTITY: any = null;
  @Input() URL: any = null;
  @Output() deleteEvent = new EventEmitter<string>();
  @Output() handleCancelEvent = new EventEmitter<string>();

  constructor(
    public baseService: BaseService<null>
  ) {
  }

  confirm() {
    const url = `${this.URL.toString() + '/' + this.ID.toString()}`;
    this.deleteEvent.emit();
    this.baseService.delete(url).subscribe(
      (res) => {
        if (res.code == '200') {
          alert('Success !');
          this.handleCancelEvent.emit();
        }
        else {
          alert(res.messageEX);
        }
      }
    );
  }

  closeModal() {
    this.handleCancelEvent.emit();
  }
}
