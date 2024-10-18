import { HttpClient } from "@angular/common/http";
import { environment } from "../../enviroments/enviroments";
import { Observable } from "rxjs";

export abstract class ServicoBase {
    private readonly apiUrl = `${environment.apiUrl}`;

    constructor(
        protected rota: string,
        protected httpClient: HttpClient){
    }

    buscarTodos(): Observable<any[]> {
        return this.httpClient.get<any[]>(`${this.apiUrl}${this.rota}`);
    }

    buscarPorCodigo(codigo: number): Observable<any> {
        return this.httpClient.get(`${this.apiUrl}${this.rota}/${codigo}`);
    } 

    cadastrar(obj: any): Observable<any> {
        return this.httpClient.post(`${this.apiUrl}${this.rota}`, obj);
    }

    atualizar(codigo: number, obj: any): Observable<any> {
        return this.httpClient.put(`${this.apiUrl}${this.rota}/${codigo}`, obj);
    }

    deletar(codigo: number): Observable<any> {
        return this.httpClient.delete(`${this.apiUrl}${this.rota}/${codigo}`);
    }

    buscar(): Observable<any>{
        return this.httpClient.get(`${this.apiUrl}${this.rota}`);
    }
}