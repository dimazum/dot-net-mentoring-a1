using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MyIoC.Exceptions;

namespace MyIoC
{
    public class Container
    {
        private readonly Dictionary<Type, Type> _registeredTypesDictionary;

        public Container()
        {
            _registeredTypesDictionary = new Dictionary<Type, Type>();

        }

        public void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                return;
            }
            foreach (var typeInfo in assembly.DefinedTypes)
            {
                foreach (ExportAttribute exportAttribute in typeInfo.GetCustomAttributes<ExportAttribute>())
                {
                    if (exportAttribute.Contract != null)
                        _registeredTypesDictionary.Add(exportAttribute.Contract, typeInfo);
                    else
                        _registeredTypesDictionary.Add(typeInfo, typeInfo);
                }
            }
        }

        public void AddType(Type type)
        {
            AddType(type, type);
        }

        public void AddType(Type type, Type baseType)
        {
            if (type == null)
                throw new NullReferenceException(type.ToString());

            if (!_registeredTypesDictionary.ContainsKey(baseType))
                _registeredTypesDictionary.Add(baseType, type);
        }

        public object CreateInstance(Type type)
        {
            if (!_registeredTypesDictionary.ContainsValue(type))
            {
                throw new UnregisteredTypeException($"Type {type} must be registered");
            }

            var isCtorInject = type
                .GetCustomAttributes(typeof(ImportConstructorAttribute), true)
                .Any();
            var isPropInject = type
                .GetProperties()
                .Any(x => x.GetCustomAttributes(typeof(ImportAttribute), true).Any());

            object instance;

            if (isCtorInject && isPropInject)
            {
                throw new Exception("More than one type registration way");
            }
            if (isCtorInject)
            {
                instance = CreateInstanceUsingCtor(type);
            }
            else if (isPropInject)
            {
                instance = CreateInstanceUsingProps(type);
            }
            else
            {
                if (GetConstructorParamsInfo(type).Length == 0)
                {
                    instance = Activator.CreateInstance(type);
                }
                else
                {
                    throw new Exception("Type has a constructor with parameters.");
                }
            }

            return instance;
        }

        public Dictionary<Type, Type> GetRegisteredTypes()
        {
            return _registeredTypesDictionary;
        }

        private object CreateInstanceUsingCtor(Type type)
        {
            var parameters = GetConstructorParamsObj(type).ToArray();

            if (parameters.Length > 0)
            {
                return Activator.CreateInstance(type, parameters);
            }

            return Activator.CreateInstance(type);
        }

        private object CreateInstanceUsingProps(Type type)
        {
            if (GetConstructorParamsInfo(type).Length > 0)
            {
                throw new Exception("Constructor must be without any parameters");
            }
            var instance = Activator.CreateInstance(type);
            var properties = type.GetProperties();
            var propertiesWithImportAttribute = properties.Where(x => x.GetCustomAttribute<ImportAttribute>() != null);

            foreach (var property in propertiesWithImportAttribute)
            {
                if (_registeredTypesDictionary.TryGetValue(property.PropertyType, out Type registeredType))
                {
                    property.SetValue(instance, CreateInstance(registeredType));
                }
                else
                {
                    throw new UnregisteredTypeException($"Type {property.PropertyType} must be registered");
                }
            }

            return instance;
        }

        private ParameterInfo[] GetConstructorParamsInfo(Type type)
        {
            var constructorInfo = type.GetConstructors()
                                      .SingleOrDefault(x => x.GetCustomAttributes(typeof(ImportConstructorAttribute), false).Any())
                                   ?? type.GetConstructors().First();
            return constructorInfo.GetParameters();
        }

        private List<object> GetConstructorParamsObj(Type type)
        {
            var ctorParams = GetConstructorParamsInfo(type);
            var paramsObjects = new List<object>();

            foreach (var parameterInfo in ctorParams)
            {
                var ctorType = parameterInfo.ParameterType;
                if (_registeredTypesDictionary.TryGetValue(ctorType, out Type registeredType))
                {
                    paramsObjects.Add(CreateInstance(registeredType));
                }
                else
                {
                    throw new UnregisteredTypeException($"Type {ctorType} must be registered");
                }
            }
            return paramsObjects;
        }

        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        public void Sample()
        {
            var container = new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());

            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(CustomerBLL2));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
            //container.AddType(typeof(Test1), typeof(ITest1));

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL2>();
        }
    }
}
