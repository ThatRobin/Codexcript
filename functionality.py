import json

def Injector(cls):
    cls.is_injector = True
    return cls


def Inject(signature):
    def decorator(func):
        func.inject_signature = signature
        return func
    return decorator


@Injector
class MyInjectorClass:
    @Inject("Attack(float, int)")
    def attack(self, a, b):
        print(f"Attacking with power: {a} and speed: {b}")

# Serialization logic
def find_annotated_classes():
    result = {}
    globals_dict = globals()
    for name, obj in globals_dict.items():
        if hasattr(obj, "is_injector") and obj.is_injector:
            # Collect methods with Inject annotations
            methods = {
                method_name: getattr(method, "inject_signature", None)
                for method_name, method in obj.__dict__.items()
                if callable(method) and hasattr(method, "inject_signature")
            }
            result[name] = methods
    return json.dumps(result)

print("hi")
if __name__ == "__main__":
    print(find_annotated_classes())