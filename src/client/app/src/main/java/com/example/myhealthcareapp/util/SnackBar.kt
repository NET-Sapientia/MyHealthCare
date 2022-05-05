package com.example.myhealthcareapp.util

import android.view.View
import com.google.android.material.snackbar.Snackbar

fun View.showSnackBar(action: () -> Unit) =
    Snackbar.make(this, "Something went wrong", Snackbar.LENGTH_LONG)
        .setAction("Retry") {
            action()
        }
        .show()