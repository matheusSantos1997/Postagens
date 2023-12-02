import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CadastrarPostComponent } from './components/posts/cadastrar-post/cadastrar-post.component';
import { ListarPostsComponent } from './components/posts/listar-posts/listar-posts.component';
import { EditarPostComponent } from './components/posts/editar-post/editar-post.component';

const routes: Routes = [
  {
    path: '', component: ListarPostsComponent
  },
  {
     path: 'post/cadastrar', component: CadastrarPostComponent
  },
  {
     path: 'post/editar/:id', component: EditarPostComponent
  },
  {
    path: '', redirectTo: '', pathMatch: 'full'
  },
  {
    path: '**', redirectTo: '', pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
