import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EffectsModule } from '@ngrx/effects';
import { CoreEffects } from './store/core.effects';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { StoreModule } from '@ngrx/store';
import { coresFeature } from './store/core.reducer';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HeaderComponent,
    FooterComponent,
    StoreModule.forFeature(coresFeature.name, coresFeature.reducer),
    EffectsModule.forFeature([CoreEffects]),
  ],
  exports: [HeaderComponent, FooterComponent],
})
export class CoreModule {}
