import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-confirmacao',
  standalone: true,
  imports: [],
  templateUrl: './modal-confirmacao.component.html',
  styleUrl: './modal-confirmacao.component.css'
})
export class ModalConfirmacaoComponent {
  
  @Input() titulo: string = "";
  @Input() mensagem: string = "";
  @Input() btnConfirmarTexto: string = "";
  @Input() btnCancelarTexto: string = "";

  constructor(private activeModal: NgbActiveModal) { }

  public decline() {
    this.activeModal.close(false);
  }

  public accept() {
    this.activeModal.close(true);
  }

  public dismiss() {
    this.activeModal.dismiss();
  }
}
