import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { Home } from './features/home/home';
import { NotFound } from './features/shared/not-found/not-found';
import { Sobre } from './public/sobre/sobre';
import { Cursos } from './features/cursos/cursos';
import { Contato } from './public/contato/contato';
import { Login } from './public/login/login';
import { Registrar } from './public/registrar/registrar';



export const routes: Routes = [
    {path:'home', component: Home}
     ,{path:'sobre', component: Sobre}
    ,{path:'lista-cursos', component: Cursos}
    ,{path:'Contato', component: Contato}
    ,{path:'login', component: Login}
    ,{path:'registrar', component: Registrar}
    ,{path:'not-found', component: NotFound}
    ,{ path: '', redirectTo: '/home', pathMatch: 'full' }
    ,{ path: '**', redirectTo: '/not-found' } 
];
