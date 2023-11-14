import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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
    public baseService: BaseService<null>,
    public toastr: ToastrService
  ) {
  }

  confirm() {
    const url = `${this.URL.toString() + '/' + this.ID.toString()}`;
    this.deleteEvent.emit();
    this.baseService.delete(url).subscribe(
      (res) => {
        if (res.code == '200') {
          this.toastr.success('Thành công !');
          this.handleCancelEvent.emit();
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
        }
      }
    );
  }

  closeModal() {
    this.handleCancelEvent.emit();
  }
}
