import {Component, ElementRef, ViewChild} from '@angular/core';
import {UserService} from "../../shared/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ActivatedRoute, Params, Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login-user.component.html',
  styleUrls: ['./login-user.component.scss']
})
export class LoginUserComponent {

  @ViewChild("myInput") myInputField!: ElementRef;
  ngAfterViewInit() {
   // this.myInputField.nativeElement.focus();
  }

  public userName: string | null;
  public lobbyId: string | null;
  public inProgress: boolean;

  constructor(private readonly userService: UserService,
              private readonly snackBar: MatSnackBar,
              private readonly router: Router,
              private activatedRoute: ActivatedRoute) {
    this.userName = null;
    this.lobbyId = null;
    this.inProgress = false;
  }

  public async loginUser(): Promise<void> {
    this.activatedRoute.params.subscribe(
      (params: Params) => {
      this.lobbyId=params['id'];
      }
      );

    this.lobbyId = this.lobbyId;
    this.inProgress = true;

    //TODO
    const nameOk = await this.userService.attempUserLogin(this.userName!, this.lobbyId!);
    this.inProgress = false;
    console.log(nameOk);
    if (nameOk){
      const navRes = await this.router.navigateByUrl(`chat`)
      console.log("past this point");
      if (!navRes){
        console.log('Navigation failed');
      }
      return;
    }
    this.userName = null;
    this.snackBar.open('Name already taken!', 'Close', {
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
  }
}
