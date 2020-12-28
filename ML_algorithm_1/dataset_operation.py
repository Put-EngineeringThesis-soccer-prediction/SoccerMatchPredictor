import pandas as pd
import abc
import numpy as np

class NoneTypeList(Exception):
    """Raise an error if list is None."""
    def __init__(self, message = None, errors = None):
        super().__init__(message)
        self.errors = errors

class DatasetOp(metaclass=abc.ABCMeta):
    """This class is a base class for dataset splitting."""
    
    def __init__(self, n_blocks, train_split, scoring):
        """Class init.        
        Parameters:
        ----------
            n_blocks : int, default = 3
                Number of blocks to create. 
            train_split : int, default = 0.8
                Percent of train set ib block. Test split is the rest of the block.
            scoring : callable, default=None
                A single callable to evaluate the predictions on the test set. 
        """
        self.n_blocks = n_blocks
        self.train_split = train_split
        self.scoring = scoring
    
    def _compute_param_dict(self, X_size, ceil = True):
        """Method computes param dictionary for batching."""
        self.param_info_ = dict()
        if ceil:
            self.param_info_['block_size'] = int(np.ceil(X_size / self.n_blocks))
        else:
            self.param_info_['block_size'] = X_size // self.n_blocks
        self.param_info_['train_size'] = int(self.param_info_['block_size'] * self.train_split)
        self.param_info_['test_size'] = self.param_info_['block_size'] - self.param_info_['train_size']
        
    def evaluate_over_batches(self, model, X, y, X_team = None, y_team = None):
        """Method splits dataset into subset with a given logic.
            Parameters
            ----------
            model : estimator object implementing 'fit'
                The object to use to fit the data.
            X : array-like of shape (n_samples, n_features)
                The data to fit. Can be for example a list, or an array.
            y : array-like of shape (n_samples,) or (n_samples, n_outputs), \
                    default=None
                The target variable to try to predict in the case of
                supervised learning.
            X_team : array-like of shape (n_samples, n_features), 
                    default = None
                The data to fit. Can be for example a list, or an array. It is data per team.
            y_team : array-like of shape (n_samples,) or (n_samples, n_outputs), \
                    default=None
                The target variable to try to predict in the case of
                supervised learning. This is tatget per team.
            Returns
            ----------
            scores : array-like of shape (n_splits,) 
                Array of scores of the estimator for each run of the cross validation.
        """
        if X is None or y is None:
            raise NoneTypeList('List can not be None.')
        if len(X) != len(y):
            raise ValueError("Lengths of list are different.")
            
        if not isinstance(X, np.ndarray):
            X = np.array(X)
        if not isinstance(y, np.ndarray):
            y = np.array(y)
            
            
        if X_team is not None and y_team is not None:
            if len(X_team) != len(y_team):
                raise ValueError("Lengths of list are different.")

            if not isinstance(X_team, np.ndarray):
                X_team = np.array(X_team)
            if not isinstance(y_team, np.ndarray):
                y_team = np.array(y_team)

            return self._train_over_batches(X, y, X_team, y_team, model)
        else:
            return self._train_over_batches(X, y, model)
        
    def _train_over_batches(self, X, y, model):
        """Method implements cross-validation training with given logic."""
        scores = list()
        for train_indices, test_indices in self.generate_cv(X, y):
            try:
                model.fit(X[train_indices], y[train_indices])
                if self.scoring is not None:
                    scores.append(self.scoring(model.predict(X[test_indices]), y[test_indices]))
            except AttributeError as e:
                print(e)
                break
        return scores
        
    @abc.abstractmethod
    def generate_cv(self, X, y):
        """Method splits dataset into subset with a given logic and yield over it. \
            This method can be used for GridSearch with cross validation.
            Parameters
            ----------
            X : array-like of shape (n_samples, n_features)
                The data to fit. Can be for example a list, or an array.
            y : array-like of shape (n_samples,) or (n_samples, n_outputs), \
                    default=None
                The target variable to try to predict in the case of
                supervised learning.
            Returns
            ----------
            train_indices : array-like of shape (n_samples, )
                Indices with which we can build our data-batch for model evaluate.
            test_indices : array-like of shape (n_samples, )
                Indices with which we can build our target-batch for model evaluate.
        """
        raise NotImplementedError("This method should be implemented.")