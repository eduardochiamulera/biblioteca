import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RelatorioServico } from '../relatorio.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-relatorio-livro',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './relatorio-autor.component.html',
  styleUrl: './relatorio-autor.component.css'
})
export class RelatorioAutorComponent {
  
  autores: Array<any> = [];
  
  constructor(private relatorioServico: RelatorioServico, private toastr: ToastrService) {
    this.carregarRelatorio();
  }

  imprimir() {
    window.print();
  }

  carregarRelatorio(): void {
    this.relatorioServico.buscar().subscribe(
      (data) => {
        this.autores = data;
      },
      (error) => {
      }
    );
  }
}
