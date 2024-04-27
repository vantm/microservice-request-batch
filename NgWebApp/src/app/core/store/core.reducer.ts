import { createFeature, createReducer, on } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { Core } from './core.model';
import { CoreActions } from './core.actions';

export const coresFeatureKey = 'cores';

export interface State extends EntityState<Core> {
  // additional entities state properties
}

export const adapter: EntityAdapter<Core> = createEntityAdapter<Core>();

export const initialState: State = adapter.getInitialState({
  // additional entity state properties
});

export const reducer = createReducer(
  initialState,
  on(CoreActions.addCore,
    (state, action) => adapter.addOne(action.core, state)
  ),
  on(CoreActions.upsertCore,
    (state, action) => adapter.upsertOne(action.core, state)
  ),
  on(CoreActions.addCores,
    (state, action) => adapter.addMany(action.cores, state)
  ),
  on(CoreActions.upsertCores,
    (state, action) => adapter.upsertMany(action.cores, state)
  ),
  on(CoreActions.updateCore,
    (state, action) => adapter.updateOne(action.core, state)
  ),
  on(CoreActions.updateCores,
    (state, action) => adapter.updateMany(action.cores, state)
  ),
  on(CoreActions.deleteCore,
    (state, action) => adapter.removeOne(action.id, state)
  ),
  on(CoreActions.deleteCores,
    (state, action) => adapter.removeMany(action.ids, state)
  ),
  on(CoreActions.loadCores,
    (state, action) => adapter.setAll(action.cores, state)
  ),
  on(CoreActions.loadCoresSuccess,
    (state, action) => adapter.setAll(action.data, state)
  ),
  on(CoreActions.loadCoresFailure,
    (state, action) => adapter.setAll([], state)
  ),
  on(CoreActions.clearCores,
    state => adapter.removeAll(state)
  ),
);

export const coresFeature = createFeature({
  name: coresFeatureKey,
  reducer,
  extraSelectors: ({ selectCoresState }) => ({
    ...adapter.getSelectors(selectCoresState)
  }),
});

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = coresFeature;
