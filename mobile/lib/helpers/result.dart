class Result<TSuccess, TFailure> {

  final TSuccess? success;
  final TFailure? _error;
  final bool _isSuccess;

  const Result.fromSuccess(this.success) : _error = null, _isSuccess = true;
  const Result.fromFailure(this._error) : success = null, _isSuccess = false;

  TResult match<TResult>(TResult Function(TSuccess) onSuccess, TResult Function(TFailure) onError) {
    return _isSuccess
        ? onSuccess(success as TSuccess)
        : onError(_error as TFailure);
  }
}