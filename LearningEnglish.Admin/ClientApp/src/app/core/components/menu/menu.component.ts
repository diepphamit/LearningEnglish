import { Component, OnInit } from '@angular/core';
import { CURRENT_USER } from 'src/app/constants/db-keys';

declare const $: any;

@Component({
    selector: 'app-menu',
    templateUrl: './menu.component.html'
})
export class MenuComponent implements OnInit {
    value: number;
    constructor() { }

    ngOnInit() {
        $("#aaa li").on("click", function () {
            $("#aaa").find(".active").removeClass("active");
            $(this).addClass("active");
        });
    }

    get name() {
        const user = JSON.parse(localStorage.getItem(CURRENT_USER));

        if (user != null) {
            return user.fullName;
        }

        return '';
    }

    get isAdmin() {
        const user = JSON.parse(localStorage.getItem(CURRENT_USER));
        if (user != null) {
            return user.roles.some(x => x === 'Admin');
        }

        return false;
    }

    get getId() {
        const user = JSON.parse(localStorage.getItem(CURRENT_USER));
        if (user != null) {
            return user.id;
        }

        return 0;
    }
    change(value: number) {
        let x = "#a" + value;
        $("#aaa").find(".active").removeClass("active");
        $(x).addClass("active");
    }
}
