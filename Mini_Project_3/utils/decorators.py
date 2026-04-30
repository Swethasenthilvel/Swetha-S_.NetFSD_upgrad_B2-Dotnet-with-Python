import functools
import time
import logging

#  Better logging format
logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s | %(levelname)s | %(message)s"
)


# -------------------------------
# LOG CALL DECORATOR
# -------------------------------
def log_call(func):
    @functools.wraps(func)
    def wrapper(*args, **kwargs):
        logging.info(f" Calling: {func.__name__}")
        result = func(*args, **kwargs)
        logging.info(f" Completed: {func.__name__}")
        return result
    return wrapper


# -------------------------------
# RETRY DECORATOR
# -------------------------------
def retry(times=3, delay=1):
    def decorator(func):
        @functools.wraps(func)
        def wrapper(*args, **kwargs):
            for attempt in range(1, times + 1):
                try:
                    return func(*args, **kwargs)
                except Exception as e:
                    logging.warning(
                        f" Attempt {attempt} failed in {func.__name__}: {e}"
                    )
                    if attempt == times:
                        logging.error(f" All retries failed for {func.__name__}")
                        raise
                    time.sleep(delay)
        return wrapper
    return decorator