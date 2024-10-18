import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AutorServico } from '../../autores/autor.service';
import { AssuntoServico } from '../../assuntos/assunto.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { LivroServico } from '../livro-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-livrocomponent',
  standalone: true,
  imports: [CommonModule, FormsModule,RouterLink, RouterLinkActive],
  templateUrl: './livro-form.component.html',
  styleUrl: './livro-form.component.css'
})
export class LivroFormComponent implements OnInit {
  livro: any = { precos: [{ formaCompra: 1, preco: 0 }, { formaCompra: 2, preco: 0 }, { formaCompra: 3, preco: 0 }] };
  autores: Array<any> = [];
  assuntos: Array<any> = [];
  livroId: number = 0;

  constructor(private livroServico: LivroServico, 
              private autorServico: AutorServico, 
              private assuntoServico: AssuntoServico, 
              private route: ActivatedRoute,
              private toastr: ToastrService, 
              private router: Router) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let livroId = params['id'];
      if (livroId == null) {

        this.buscarAssuntos();
        this.buscarAutores();
        return;
      }
      this.livroId = livroId;
      this.buscarPorCodigo();
    });
  }

  buscarAutores(): void {
    this.autorServico.buscarTodos().subscribe(
      (data: Array<any>) => {
        if (this.livro.autores != null) {
          data.forEach(a => a.checked = (this.livro.autores as Array<any>).some(au => au.id == a.id));
        }
        this.autores = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao obter a lista de autores');
      }
    );
  }

  buscarAssuntos(): void {
    this.assuntoServico.buscarTodos().subscribe(
      (data: Array<any>) => {
        if (this.livro.assuntos != null) {
          data.forEach(a => a.checked = (this.livro.assuntos as Array<any>).some(au => au.id == a.id));
        }
        this.assuntos = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao obter a lista de assuntos');
      }
    );
  }

  buscarPorCodigo(): void {
    this.livroServico.buscarPorCodigo(this.livroId).subscribe(
      (data) => {
        this.livro = data;

        this.buscarAutores();
        this.buscarAssuntos();
      },
      (error) => {
      }
    );
  }

  finalizar(): void {
    if(!this.livro.titulo || this.livro.titulo.trim() === '') {
      this.toastr.error('Campo título é obrigatório');
      return;
    }

    if(this.livro.editora == null || this.livro.editora.trim() == '') {
      this.toastr.error('Campo editora é obrigatório');
      return;
    }

    if(this.livro.edicao == null) {
      this.toastr.error('Campo edição é obrigatório');
      return;
    }

    if(this.livro.anoPublicacao == null) {
      this.toastr.error('Campo ano publicação é obrigatório');
      return;
    }
    
    let autores = this.autores.filter((autor) => autor.checked);
    this.livro.autoresIds = autores.map(a => a.id);

    let assuntos = this.assuntos.filter((assunto) => assunto.checked);
    this.livro.assuntosIds = assuntos.map(a => a.id);

    if (this.livroId == 0) {
      this.handleResponse(this.livroServico.cadastrar(this.livro));
      return;
    }

    this.handleResponse(this.livroServico.atualizar(this.livroId, this.livro));
  }

  handleResponse(request: Observable<any>): any {
    request.subscribe(
      (data) => {
        this.toastr.success('Livro salvo com sucesso!');
        window.setTimeout(() => this.router.navigate(['/livros']), 1000);
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao salvar');
      }
    );
  }

  validaAno(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = input.value;
  
    input.value = value.replace(/[^0-9]/g, '');
  }

}
