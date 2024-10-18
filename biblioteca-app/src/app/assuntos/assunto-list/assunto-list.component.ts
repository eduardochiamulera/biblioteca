import { Component, OnInit } from '@angular/core';
import { AssuntoServico } from '../assunto.service';
import { ToastrService } from 'ngx-toastr';
import { ModalConfirmacaoServico } from '../../core/modal-confirmacao-service';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-assunto-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './assunto-list.component.html',
  styleUrl: './assunto-list.component.css',
  providers: [ModalConfirmacaoServico]
})
export class AssuntoListComponent implements OnInit {

  assuntos: any[] = [];

  constructor(private assuntoServico: AssuntoServico,
              private modalConfirmacaoServico: ModalConfirmacaoServico,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.carregar();
  }

  carregar(): void {
    this.assuntoServico.buscarTodos().subscribe(
      (data) => {
        this.assuntos = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao obter a lista');
      }
    );
  }

  confirmarExcluir(id: number){
    this.modalConfirmacaoServico.confirm('Continuar..', 'Tem certeza que deseja deletar o assunto?', 'Confirmar', 'Cancelar', 'lg')
    .then((confirmado) => {
      if(confirmado){
        this.excluir(id);
      }
    })
    .catch(() => { return; });
  }

  private excluir(id: number) {
    this.assuntoServico.deletar(id).subscribe(
      (data) => {
        this.toastr.error('Deletado com sucesso');
        this.assuntos = this.assuntos.filter( x => x.id !== id)
      },
      (error) => {
        this.toastr.error(error.error);
      }
    );;
  }
}
