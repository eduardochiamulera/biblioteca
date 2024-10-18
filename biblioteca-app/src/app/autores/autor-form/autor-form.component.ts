import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AutorServico } from '../autor.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-autor',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './autor-form.component.html',
  styleUrl: './autor-form.component.css'
})
export class AutorFormComponent {

  autor: any = {};
  autorId: number = 0;

  constructor(private autorService: AutorServico, private route: ActivatedRoute,
    private toastr: ToastrService, private router: Router) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let assuntoId = params['id'];
      if (assuntoId == null) return;
      this.autorId = assuntoId;
      this.buscarPorCodigo();
    });
  }

  buscarPorCodigo(): void {
    this.autorService.buscarPorCodigo(this.autorId).subscribe(
      (data) => {
        this.autor = data;
      },
      (error) => {
      }
    );
  }

  finalizar(): void {
    if (this.autor.nome == null || this.autor.descricao == '') {
      this.toastr.error('Campo nome é obrigatório.');
      return;
    }

    if (this.autorId == 0) {
      this.handleResponse(this.autorService.cadastrar(this.autor));
      return;
    }

    this.handleResponse(this.autorService.atualizar(this.autorId, this.autor));
  }

  handleResponse(request: Observable<any>): any {
    request.subscribe(
      (data) => {
        this.toastr.success('Autor salvo com sucesso!');
        window.setTimeout(() => this.router.navigate(['/autores']), 1000);
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao salvar');
      }
    );
  }
}
