from dataset_operation import DatasetOp
class StartSplit(DatasetOp):
    """This class splits dataset into blocks from begining of list to margin points."""
    def __init__(self, **kwargs):
        super().__init__(**kwargs)    
    
    def generate_cv(self, X, y):
        self._compute_param_dict(len(X))

        for epoch in range(self.n_blocks):
            margin = self.param_info_['block_size'] + (epoch * self.param_info_['test_size'])
            train_indices = range(0, margin)
            if epoch == self.n_blocks - 1:
                end = len(X)
            else:
                end = margin + self.param_info_['test_size']
            test_indices = range(margin, end)
            yield list(train_indices), list(test_indices)
            