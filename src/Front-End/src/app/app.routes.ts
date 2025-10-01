import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { Home } from './features/home/home';
import { NotFound } from './features/shared/not-found/not-found';
import { Sobre } from './public/sobre/sobre';
import { Cursos } from './features/cursos/cursos';
import { Contato } from './public/contato/contato';
import { Login } from './public/login/login';
import { Registrar } from './public/registrar/registrar';
import { Ementa } from './aluno/ementa/ementa';
import { PainelAdministrativo } from './administrador/painel-administrativo/painel-administrativo';
import { CadastrarCurso } from './administrador/cadastrar-curso/cadastrar-curso';
import { CadastrarAula } from './administrador/cadastrar-aula/cadastrar-aula';
import { GerenciarAulas } from './administrador/gerenciar-aulas/gerenciar-aulas';
import { MeusCursos } from './administrador/meus-cursos/meus-cursos';
import { authGuard } from './core/auth/guards/auth.guard';

export const routes: Routes = [
  { path: 'home', component: Home }
  , { path: 'sobre', component: Sobre }
  , { path: 'lista-cursos', component: Cursos }
  , { path: 'Contato', component: Contato }
  , { path: 'login', component: Login }
  , { path: 'registrar', component: Registrar }
  , { path: 'ementa', component: Ementa }

  , { path: 'painel-administrador', component: PainelAdministrativo, canActivate: [authGuard] }
  , { path: 'cadastrar-curso', component: CadastrarCurso, canActivate: [authGuard] } // Rota para criar um novo curso
  , { path: 'cadastrar-aula', component: CadastrarAula, canActivate: [authGuard] } // Rota para adicionar aula a um curso existente
  , { path: 'gerenciar-aulas', component: GerenciarAulas, canActivate: [authGuard] }
  , { path: 'meus-cursos', component: MeusCursos, canActivate: [authGuard] }
  , { path: 'not-found', component: NotFound }


  , { path: '', redirectTo: '/home', pathMatch: 'full' }
  , { path: '**', redirectTo: '/not-found' }
];
