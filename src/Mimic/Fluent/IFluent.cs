﻿namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IFluent
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    Type GetType();

    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetHashCode();

    [EditorBrowsable(EditorBrowsableState.Never)]
    string ToString();

    [EditorBrowsable(EditorBrowsableState.Never)]
    bool Equals(object obj);
}
