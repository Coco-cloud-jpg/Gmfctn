import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { AchievementServiceService } from 'src/app/core/services/achievement-service/achievement-service.service';
import { Achievement } from '../../models/achievement';

@Component({
  selector: 'app-request-modal',
  templateUrl: './request-modal.component.html',
  styleUrls: ['./request-modal.component.scss']
})
export class RequestModalComponent implements OnInit, OnDestroy {
  request = {
    message: '',
    achievementId: '',
  };
  userForm: FormGroup = new FormGroup({});
  achList: Achievement[] = [];
  subscription = new Subscription();

  constructor(private readonly fb: FormBuilder, private dialogRef: MatDialogRef<RequestModalComponent>,
              private achievementServiceService: AchievementServiceService) { }

  ngOnInit(): void {
    this.userForm = this.fb.group({
      achievement: this.fb.control(this.request.achievementId, Validators.required),
      message: this.fb.control(this.request.message, Validators.required),
    });
    this.subscription.add(this.achievementServiceService.getAllAchievements().subscribe(achhievements => this.achList = achhievements));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  submit(): void {
    if (this.userForm.valid) {
        this.request = this.userForm.value;
        this.request.achievementId = this.achList.find(achievement => achievement.name === this.userForm.value.achievement)?.id ?? '';
        this.subscription.add(this.achievementServiceService.sendRequest(this.request).subscribe());
        this.close();
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
