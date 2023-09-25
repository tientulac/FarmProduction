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
  // @Output() deleteEvent = new EventEmitter<string>();
  @Output() handleCancelEvent = new EventEmitter<string>();

  constructor(
    public baseService: BaseService<null>
  ) {
  }

  confirm() {
    // this.deleteEvent.emit();
    // this.baseService.delete(this.URL + this.ID).subscribe(
    //   (res) => {
    //     if (res.StatusCode == 200) {
    //       alert('Success !');
    //     }
    //     else {
    //       alert('Faied !');
    //     }
    //   }
    // );
    this.handleCancelEvent.emit();
  }

  closeModal(){
    this.handleCancelEvent.emit();
  }
}
