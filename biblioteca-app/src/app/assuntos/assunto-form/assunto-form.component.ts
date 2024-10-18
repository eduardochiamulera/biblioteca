import { Component } from '@angular/core';
import { AssuntoServico } from '../assunto.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Router } from "@angular/router"
import { Observable, catchError, tap } from 'rxjs';

@Component({
  selector: 'app-assunto.form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './assunto-form.component.html',
  styleUrl: './assunto-form.component.css'
})
export class AssuntoFormComponent {

  assunto: any = {};
  assuntoId: number = 0;

  constructor(private assuntoService: AssuntoServico, private route: ActivatedRoute,
    private toastr: ToastrService, private router: Router) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let assuntoId = params['id'];
      if (assuntoId == null) {
        return;
      }

      this.assuntoId = assuntoId;
      this.getById();
    });
  }

  getById(): void {
    this.assuntoService.buscarPorCodigo(this.assuntoId).subscribe(
      (data) => {
        this.assunto = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao buscar o assunto');
      }
    );
   
  }

  submit(): void {
    if (this.assunto.descricao == null || this.assunto.descricao == '') {
      this.toastr.error('Campo descrição é obrigatório');
      return;
    }

    if (this.assuntoId == 0) {
      this.handleResponse(this.assuntoService.cadastrar(this.assunto));
      return;
    }

    this.handleResponse(this.assuntoService.atualizar(this.assuntoId, this.assunto));
  }

  handleResponse(request: Observable<any>): any {
    request.subscribe(
      (data) => {
        this.toastr.success('Assunto salvo com sucesso!');
        window.setTimeout(() => this.router.navigate(['/assuntos']), 1000);
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao salvar');
      }
    );
  }
}