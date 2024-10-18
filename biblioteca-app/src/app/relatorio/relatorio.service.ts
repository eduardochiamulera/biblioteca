import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ServicoBase } from '../core/servico-base.service';

@Injectable({
  providedIn: 'root'
})
export class RelatorioServico extends ServicoBase {
    
    constructor(protected http: HttpClient) {
      super("/relatorios/autores", http);
    }
}