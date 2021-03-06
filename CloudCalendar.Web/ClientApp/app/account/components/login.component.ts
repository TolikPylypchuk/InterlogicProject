import { Component, OnInit, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";

import { AccountService } from "../services/account.service";
import { LoginModel } from "../models/models";

@Component({
	selector: "login",
	templateUrl: "./login.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class LoginComponent implements OnInit, OnDestroy {
	model: LoginModel = {
		username: "",
		password: ""
	};

	emailDomain = "";

	loginError = false;

	private router: Router;
	private accountService: AccountService;

	constructor(router: Router, accountService: AccountService) {
		this.router = router;
		this.accountService = accountService;
	}

	ngOnInit(): void {
		this.accountService.setLoggingIn(true);

		this.accountService.getEmailDomain()
			.subscribe(domain => this.emailDomain = domain);
	}

	ngOnDestroy(): void {
		this.accountService.setLoggingIn(false);
	}

	getEmailDomain(): string {
		return this.emailDomain;
	}

	returnUrl(): string {
		const result = this.accountService.getReturnUrl();
		return result ? result : "";
	}

	change(): void {
		this.loginError = false;
	}

	submit(): void {
		this.accountService.login(this.model)
			.subscribe(
				(success: boolean) => {
					if (!success) {
						this.loginError = true;
						this.model.password = "";
					}
				},
				() => {
					this.loginError = true;
					this.model.password = "";
				});
	}

	cancel(): void {
		this.router.navigate([ this.returnUrl() ]);
	}
}
