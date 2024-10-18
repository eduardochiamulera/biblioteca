import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ServicoBase } from '../core/servico-base.service';

@Injectable({
  providedIn: 'root'
})
export class LivroServico extends ServicoBase {
    
    constructor(protected http: HttpClient) {
      super("/Livros", http);
    }
}