import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { Home } from './features/home/home';
import { NotFound } from './features/shared/not-found/not-found';
import { Sobre } from './public/sobre/sobre';



export const routes: Routes = [
    {path:'home', component: Home}
     ,{path:'sobre', component: Sobre}
    ,{path:'not-found', component: NotFound}
    ,{ path: '', redirectTo: '/home', pathMatch: 'full' }
    ,{ path: '**', redirectTo: '/not-found' } 
];
