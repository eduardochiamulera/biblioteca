import { Routes } from '@angular/router';
import { AssuntoFormComponent } from './assuntos/assunto-form/assunto-form.component';
import { AssuntoListComponent } from './assuntos/assunto-list/assunto-list.component';
import { AutorFormComponent } from './autores/autor-form/autor-form.component';
import { AutorListComponent } from './autores/autor-list/autor-list.component';
import { LivroListComponent } from './livros/livro-list/livro-list.component';
import { LivroFormComponent } from './livros/livro-form/livro-form.component';
import { RelatorioAutorComponent } from './relatorio/relatorio-livro/relatorio-autor.component';

export const routes: Routes = [
    { path: 'livros', component: LivroListComponent },
    { path: 'livros/novo', component: LivroFormComponent },
    { path: 'livros/edit/:id', component: LivroFormComponent },
    
    { path: 'autores', component: AutorListComponent },
    { path: 'autores/novo', component: AutorFormComponent },
    { path: 'autores/edit/:id', component: AutorFormComponent },
    
    { path: 'assuntos', component: AssuntoListComponent },
    { path: 'assuntos/novo', component: AssuntoFormComponent },
    { path: 'assuntos/edicao/:id', component: AssuntoFormComponent },

    { path: 'relatorio', component: RelatorioAutorComponent }
];
