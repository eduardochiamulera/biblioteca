import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LivroServico } from '../livro-service';
import { ToastrService } from 'ngx-toastr';
import { ModalConfirmacaoService } from '../../core/modal-confirmacao-service';

@Component({
  selector: 'app-livro-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './livro-list.component.html',
  styleUrl: './livro-list.component.css',
  providers: [ModalConfirmacaoService]
})
export class LivroListComponent implements OnInit {
  livros: any[] = [];
  
  constructor(private livroServico: LivroServico, 
              private toastr: ToastrService,
              private modalConfirmacaoService: ModalConfirmacaoService) {
  }

  ngOnInit(): void {
    this.buscarTodos();
  }

  buscarTodos(): void {
    this.livroServico.buscarTodos().subscribe(
      (data) => {
        this.livros = data;
      },
      (error) => {
        this.toastr.error("Erro ao consultar livros");
        console.error('Erro ao consultar livros:', error);
      }
    );
  }

  confirmarExcluir(id: number){
    this.modalConfirmacaoService.confirm('Continuar..', 'Tem certeza que deseja deletar o livro?', 'Confirmar', 'Cancelar', 'lg')
    .then((confirmado) => {
      if(confirmado){
        this.excluir(id);
      }
    })
    .catch(() => { return; });
  }

  excluir(id: number) {
    this.livroServico.deletar(id).subscribe(
      (data) => {
        this.toastr.error('Deletado com sucesso');
        this.livros = this.livros.filter( x => x.codigo !== id );
      },
      (error) => {
        console.log(error)
        this.toastr.error('Ocorreu um erro ao deletar');
      }
    );;
  }
}
