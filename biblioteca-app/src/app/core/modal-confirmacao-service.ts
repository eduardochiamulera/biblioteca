import { Injectable } from '@angular/core';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { ModalConfirmacaoComponent } from './modal-confirmacao/modal-confirmacao.component';

@Injectable()
export class ModalConfirmacaoService {

  constructor(private modalService: NgbModal) { }

  public confirm(
    titulo: string,
    mensagem: string,
    btnConfirmarTexto: string = 'Confirmar',
    btnCancelarTexto: string = 'Cancelar',
    tamanhoModal: 'sm'|'lg' = 'sm'): Promise<boolean> {
    const modalRef = this.modalService.open(ModalConfirmacaoComponent, { size: tamanhoModal });
    modalRef.componentInstance.titulo = titulo;
    modalRef.componentInstance.mensagem = mensagem;
    modalRef.componentInstance.btnConfirmarTexto = btnConfirmarTexto;
    modalRef.componentInstance.btnCancelarTexto = btnCancelarTexto;

    return modalRef.result;
  }

}