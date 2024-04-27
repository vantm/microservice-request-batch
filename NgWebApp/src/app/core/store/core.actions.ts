import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { Update } from '@ngrx/entity';

import { Core } from './core.model';

export const CoreActions = createActionGroup({
  source: 'Core/API',
  events: {
    'Load Cores': props<{ cores: Core[] }>(),
    'Load Cores Success': props<{ data: Core[] }>(),
    'Load Cores Failure': props<{ error: any }>(),
    'Add Core': props<{ core: Core }>(),
    'Upsert Core': props<{ core: Core }>(),
    'Add Cores': props<{ cores: Core[] }>(),
    'Upsert Cores': props<{ cores: Core[] }>(),
    'Update Core': props<{ core: Update<Core> }>(),
    'Update Cores': props<{ cores: Update<Core>[] }>(),
    'Delete Core': props<{ id: string }>(),
    'Delete Cores': props<{ ids: string[] }>(),
    'Clear Cores': emptyProps(),
  },
});
