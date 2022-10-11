import {
    transition,
    trigger,
    query,
    style,
    animate,
    group,
    animateChild,
    keyframes
} from '@angular/animations';


export const slideInAnimation =
    trigger('routeAnimations', [
        transition('CRUD => *', [
            query(':enter, :leave', style({ position: 'fixed', width: '100%' }), { optional: true }),
            group([
                query(':enter', [
                    style({ transform: 'translateX(-100%)' }),
                    animate('0.7s ease-in-out', style({ transform: 'translateX(0%)' }))
                ], { optional: true }),
                query(':leave', [
                    style({ transform: 'translateX(0%)' }),
                    animate('0.7s ease-in-out', style({ transform: 'translateX(100%)' }))
                ], { optional: true }),
            ])
        ]),

        transition('* => CRUD', [
            query(':enter, :leave', style({ position: 'fixed', width: '100%' }), { optional: true }),
            group([
                query(':enter', [
                    style({ transform: 'translateX(100%)' }),
                    animate('0.7s ease-in-out', style({ transform: 'translateX(0%)' }))
                ], { optional: true }),
                query(':leave', [
                    style({ transform: 'translateX(0%)' }),
                    animate('0.7s ease-in-out', style({ transform: 'translateX(-100%)' }))
                ], { optional: true }),
            ])
        ]),
        transition('* => Login', [
            query(':enter, :leave', style({ position: 'fixed', width: '100%' }), { optional: true }),
            group([
                query(':enter', [
                    
                ], { optional: true }),
                query(':leave', [
                    
                ], { optional: true }),
            ])
        ]),
        transition('* <=> *', [
            query(':enter, :leave', style({ position: 'fixed', width: '100%' }), { optional: true }),
            group([
                query(':enter', [
                    
                    style({ transform: 'translateY(-100%)' }),
                    animate('0.7s ease-in-out', style({ transform: 'translateY(0%)' }))
                ], { optional: true }),
                query(':leave', [
                    style({ visibility: 'hidden' }),
                    
                ], { optional: true }),
            ])
        ]),
        transition('otro <=> otro', [
            animate('0.5s 0s ease-in',
                keyframes([
                    style({
                        transform: 'perspective(400px) rotateY(0deg)',
                        offset: 0
                    }),
                    style({
                        transform: 'perspective(400px) scale3d(1.5, 1.5, 1.5) rotateY(-80deg)',
                        offset: 0.4
                    }),
                    style({
                        transform: 'perspective(400px) scale3d(1.5, 1.5, 1.5) rotateY(-100deg)',
                        offset: 0.5
                    }),
                    style({
                        transform: 'perspective(400px) scale3d(0.95, 0.95, 0.95) rotateY(-180deg)',
                        offset: 0.8
                    }),
                    style({
                        transform: 'perspective(400px) scale3d(1, 1, 1) rotateY(-180deg)',
                        offset: 1
                    })
                ]))]),
    ])

