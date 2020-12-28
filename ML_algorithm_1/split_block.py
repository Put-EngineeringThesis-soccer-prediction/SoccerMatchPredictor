from dataset_operation import DatasetOp

class BlockingSplit(DatasetOp):
    """This class splits dataset into blocks from one margin to next margin and make blocks as consecutive ones."""
    def __init__(self, n_blocks = 3, train_split = 0.8, scoring = None):
        super().__init__(n_blocks, train_split, scoring)
    
    def generate_cv(self, X, y):
        self._compute_param_dict(len(X))
        
        for epoch in range(self.n_blocks):
            margin = epoch * self.param_info_['train_size']
            train_indices = range(margin, margin + self.param_info_['train_size'])
            if epoch == self.n_blocks - 1:
                end = len(X)
            else:
                end = margin + self.param_info_['train_size'] + self.param_info_['test_size']
            test_indices = range( margin + self.param_info_['train_size'], end)
            
            yield list(train_indices), list(test_indices)