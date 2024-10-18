import { Component, OnInit } from '@angular/core';
import { AutorServico } from '../autor.service';
import { ToastrService } from 'ngx-toastr';
import { ModalConfirmacaoServico } from '../../core/modal-confirmacao-service';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-autor-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './autor-list.component.html',
  styleUrl: './autor-list.component.css',
  providers: [ModalConfirmacaoServico]
})
export class AutorListComponent implements OnInit {

  autores: any[] = [];

  constructor(private autorServico: AutorServico,
              private modalConfirmacaoServico: ModalConfirmacaoServico,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.carregar();
  }

  carregar(): void {
    this.autorServico.buscarTodos().subscribe(
      (data) => {
        this.autores = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao obter a lista');
      }
    );
  }

  confirmarExcluir(id: number){
    this.modalConfirmacaoServico.confirm('Continuar..', 'Tem certeza que deseja deletar o autor?', 'Confirmar', 'Cancelar', 'lg')
    .then((confirmado) => {
      if(confirmado){
        this.excluir(id);
      }
    })
    .catch(() => { return; });
  }

  private excluir(id: number) {
    this.autorServico.deletar(id).subscribe(
      (data) => {
        this.toastr.error('Deletado com sucesso');
        this.autores = this.autores.filter( x => x.id !== id)
      },
      (error) => {
        this.toastr.error(error.error);
      }
    );;
  }
}
